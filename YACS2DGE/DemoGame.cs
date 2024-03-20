using System;
using System.Collections.Generic;
using YACS2DGE.YACS2DGE;

namespace YACS2DGE {
  class DemoGame : YACS2DGE.YACS2DGE {
    
    public DemoGame() : base(new Vector2(640, 480), "YACS2DGE Demo Project", 60) {  }

    //Sprite2D player;
    Player2D player;
    Rect2D gun;

    int MoveSpeed = 5;

    int ReloadTime = 15;
    int CurReloadTime;

    List<Rect2D> bullets;

    public override void OnLoad()
    {
      //player = new Sprite2D(new Vector2(ScreenSize.x / 2, ScreenSize.y / 2), new Vector2(1, 1), "Player", "Assets/Sprites/Sprite-Test.png");
      player = new Player2D(new Vector2(0, 0), new Vector2(1, 1), "Player", "Assets/Sprites/player_spritesheet.png", new Vector2(16,16));

      gun = new Rect2D(new Vector2(0, 0), new Vector2(48, 10), "AimyThingy", new Color(0,0,0));
      gun.PivotPoint = new Vector2(-gun.Scale.x / 2, gun.PivotPoint.y);

      bullets = new List<Rect2D>();
      CurReloadTime = ReloadTime;
    }

    public override void OnUpdate(){
       // Calculate the angle between the sprite and the mouse
      float angle = (float)Math.Atan2(MousePosition.y - (player.Position.y * 2), MousePosition.x - (player.Position.x * 2));
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
        }
      }
    }

    public override void OnDraw(){
      // if you want to manually draw things
    }

    public override void BeforeLoad(){
      // idk the title is self explanatory
    }

    public void Shoot(){
      Rect2D bullet = new Rect2D(new Vector2(gun.Position.x, gun.Position.y), new Vector2(5, 5), "Bullet", new Color(155, 155, 155));
      bullet.Rotation = gun.Rotation;
      bullets.Add(bullet);
    }
  }
}
