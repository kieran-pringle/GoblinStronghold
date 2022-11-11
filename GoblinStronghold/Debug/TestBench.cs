using System;
using GoblinStronghold.ECS;
using GoblinStronghold.Graphics.Systems;
using GoblinStronghold.Maps.Entities;
using GoblinStronghold.Messaging;
using GoblinStronghold.Physics.Components;

namespace GoblinStronghold.Debug
{
    public class TestBench
    {
        public static void Load(IContext context, RootScreen screen)
        {
            var camera = new CameraSystem(screen.MapConsole());

            context.Register(camera);

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
        }
    }
}

