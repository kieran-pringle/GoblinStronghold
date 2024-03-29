﻿using System;
using GoblinStronghold.Creatures.Entities;
using GoblinStronghold.ECS;
using GoblinStronghold.Input;
using GoblinStronghold.Graphics;
using GoblinStronghold.Graphics.Systems;
using GoblinStronghold.Maps.Entities;
using GoblinStronghold.Physics.Components;
using GoblinStronghold.Physics;
using static GoblinStronghold.ECS.Context;

namespace GoblinStronghold.Debug
{
    /**
     *  Puts a player in a room with a bat for some very basic testing
     */
    public class SingleRoomBench
    {
        public static void Load(IContext context, RootScreen screen)
        {
            InitSystems(context, screen);

            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    if (x == 0 || y == 0 || x == 15 || y == 15)
                    {
                        Wall.NewIn(context).With(new Position(x, y));
                    }
                    else
                    {
                        Floor.NewIn(context).With(new Position(x, y));
                    }
                }
            }

            Player.NewIn(context).With(new Position(10, 10));
            Bat.NewIn(context).With(new Position(4, 4));
        }

        public static void InitSystems(IContext context, RootScreen screen)
        {
            Graphics.Constants.Init(context, screen);
            Input.Constants.Init(context);
            Physics.Constants.Init(context);
            Time.Constants.Init(context);
        }
    }
}

