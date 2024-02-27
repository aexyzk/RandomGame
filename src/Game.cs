using System;
using System.Collections.Generic;
using YACS2DGE.YACS2DGE;

namespace YACS2DGE{
    class Game : YACS2DGE.YACS2DGE {
        public Game() : base(new Vector2(640, 480), "Mutiplayer Top Down Shooter", 60) {  }

        int MoveSpeed = 5;
        int ReloadTime = 15;
        int CurReloadTime;
        Sprite2D player;
        Rect2D gun;
        List<Rect2D> bullets;

        public override void OnLoad()
        {
            player = new Sprite2D(new Vector2(ScreenSize.x / 2, ScreenSize.y / 2), new Vector2(1, 1), "Player", "Assets/Sprites/Sprite-Test.png");
            gun = new Rect2D(new Vector2(ScreenSize.x / 2, ScreenSize.y / 2), new Vector2(48, 10), "AimyThingy", new Color(0,0,0));
            gun.PivotPoint = new Vector2(-gun.Scale.x / 2, gun.PivotPoint.y);

            bullets = new List<Rect2D>();
            CurReloadTime = ReloadTime;

            Level.InitLevel();
        }

        public override void OnUpdate(){
            // Calculate the angle between the sprite and the mouse
            float angle = (float)Math.Atan2(MousePosition.y - player.Position.y, MousePosition.x - player.Position.x);
            angle = angle * (180.0f / MathF.PI); // Convert radians to degrees
            gun.Rotation = angle;

            player.Position.x += HorizontalAxis * MoveSpeed;
            player.Position.y += VerticalAxis * MoveSpeed;

            gun.Position = player.Position;
            
            if (CurReloadTime > ReloadTime){
                if (Mouse1Down){
                Shoot();
                CurReloadTime = 0;
                }
            }else{
                CurReloadTime += 1;
            }

            if (bullets != null){
                // Update bullet positions
                foreach (var bullet in bullets){
                    float bulletSpeed = 8.0f;
                    bullet.Position.x += MathF.Cos(bullet.Rotation * (MathF.PI / 180.0f)) * bulletSpeed;
                    bullet.Position.y += MathF.Sin(bullet.Rotation * (MathF.PI / 180.0f)) * bulletSpeed;
                    bullet.Tick();
                }
            }
        }

        public override void OnDraw(){

        }

        public override void BeforeLoad(){

        }

        public void Shoot(){
            Rect2D bullet = new Rect2D(new Vector2(gun.Position.x, gun.Position.y), new Vector2(10, 10), "Bullet", new Color(200, 200, 200), 15);
            bullet.Rotation = gun.Rotation;
            bullets.Add(bullet);
        }
    }
}