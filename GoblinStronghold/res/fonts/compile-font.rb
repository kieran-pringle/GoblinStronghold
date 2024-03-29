﻿#!/usr/bin/env ruby

require 'yaml'
require 'json'
require 'chunky_png'

@fontNames = ["1-bit_16x16", "ascii_8x16"]

@logLevel = 2

@font = ARGV[0]
unless @font
    raise "No font passed as argument"
    unless @fontNames.include?(@font)
        raise "#{@font} is not a valid font. Must be one of #{@fontNames}"
    end
end

@baseFile = "#{@font}.yaml"
@rawPng = "#{@font}_raw.png"

def indentLog(indent, msg)
    puts (" "*indent*2) + msg unless indent > @logLevel
end

Dir.chdir(@font)
indentLog(0, "=== checking dir ===")
unless (File.file?(@baseFile))
    puts Dir["*"]
    raise "Can't find font. Was looking for #{@baseFile}. Working dir was #{Dir.pwd}"
end

@files = Dir["GlyphDefinitions/*.yaml"]

@parsedFiles = Hash.new
indentLog(0, "")
indentLog(0, "=== reading files ===")
@files.each do |f|
    indentLog(1,"Parsing #{f}")
    @parsedFiles[File.basename(f)] = YAML.load_file(f)
end

indentLog(0, "")
indentLog(0, "=== processing definitions ===")

@glyphIndexToString = Hash.new
@glyphStringToIndex = Hash.new
@transformedGlyphDefinitions = Hash.new

@parsedFiles.each do |filename, content|
    indentLog(1, "Processing file #{filename}")
    content["GlyphDefinitions"].each do |glyphDef|
        idx = glyphDef["StartingAt"]
        indentLog(2,"Processing '#{glyphDef["Name"]}' starting at index #{idx}")
        prefix = glyphDef["Prefix"] || ""
        suffix = glyphDef["Suffix"] || ""
        
        glyphDef["Glyphs"].each do |glyph|
            # check keys
            fullGlyphName = "#{prefix}-#{glyph}-#{suffix}"
                .delete_prefix("-")
                .delete_suffix("-")
            isIndexUsed = @glyphIndexToString.key?(idx)
            isNameUsed = @glyphStringToIndex.key?(fullGlyphName)

            errMsg = ""
            if (isIndexUsed)
                errMsg << "Index #{idx} already in use for glyph "
                errMsg << "'#{@glyphIndexToString[idx]}'. "
            end
            if (isNameUsed)
                errMsg << "Name #{fullGlyphName} already in use for index "
                errMsg << "#{idx}. "
            end
            if (isIndexUsed || isNameUsed)
                raise "Cannot assign '#{fullGlyphName}' to #{idx}: " + errMsg
            end

            @glyphIndexToString[idx] = fullGlyphName
            @glyphStringToIndex[fullGlyphName] = idx
            indentLog(3, "#{idx}: #{glyph}")

            @transformedGlyphDefinitions[fullGlyphName] = {
                Glyph: idx,
                Mirror: 0
            }

            idx += 1
        end
    end
end

indentLog(0, "")
indentLog(0, "=== building .font file ===")

indentLog(1, "loading #{@baseFile}")
@fontDefinition = YAML.load_file(@baseFile)
@fontDefinition["GlyphDefinitions"] = @transformedGlyphDefinitions

outputFile = @fontDefinition["Name"] + ".font"
indentLog(1, "writing result to #{outputFile}")
File.write(
    outputFile,
    JSON.pretty_generate(@fontDefinition))

indentLog(0, "")
indentLog(0, "=== processing .png ===")

@transparentBlack = ChunkyPNG::Color.rgba(0,0,0,0)
@image = ChunkyPNG::Image.from_file(@rawPng)

(0..@image.width-1).each do |x|
    (0..@image.height-1).each do |y|
        pixel = @image[x,y]
        if pixel == ChunkyPNG::Color::BLACK
            @image[x,y] = ChunkyPNG::Color::TRANSPARENT
        end
    end
end

outputPngPath = @fontDefinition["FilePath"]
indentLog(1, "writing #{outputPngPath}")
@image.save(outputPngPath)   

indentLog(0, "")
indentLog(0, "=== DONE! ===")
