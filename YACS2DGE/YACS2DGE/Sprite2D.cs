namespace YACS2DGE.YACS2DGE
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 PivotPoint = null;
        public float Rotation = 0f;
        public Vector2 Scale = null;
        public string Tag = "";
        public string SpriteTextureFileName = "Assets/Sprites/Sprite-Test.png";

        public Sprite2D(Vector2 Position, Vector2 Scale, string Tag, string SpriteTextureFileName)
        {
            this.Position = new Vector2(Position.x, Position.y);
            this.Rotation = 0;
            this.Scale = new Vector2(Scale.x, Scale.y);
            this.Tag = Tag;
            this.SpriteTextureFileName = SpriteTextureFileName;

            Log.Info($"Created Sprite2D: {Tag}");
            YACS2DGE.RegisterSprite2D(this);
        }

        public void Destroy(){
            YACS2DGE.UnregisterSprite2D(this);
        }
    }
}