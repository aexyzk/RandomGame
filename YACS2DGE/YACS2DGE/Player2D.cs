namespace YACS2DGE.YACS2DGE
{
    public class Player2D
    {
        public Vector2 Position = null;
        public Vector2 PivotPoint = null;
        public float Rotation = 0f;
        public Vector2 Scale = null;
        public string Tag = "";
        public string SpriteTextureFileName = "Assets/Sprites/Sprite-Test.png";
        public Vector2 CellSize = null;

        public Player2D(Vector2 Position, Vector2 Scale, string Tag, string SpriteTextureFileName, Vector2 CellSize)
        {
            this.Position = new Vector2(Position.x, Position.y);
            this.Rotation = 0;
            this.Scale = new Vector2(Scale.x, Scale.y);
            this.Tag = Tag;
            this.SpriteTextureFileName = SpriteTextureFileName;
            this.CellSize = CellSize;

            Log.Info($"Created Sprite2D: {Tag}");
            YACS2DGE.RegisterPlayer2D(this);
        }

        public void Destroy(){
            YACS2DGE.UnregisterPlayer2D(this);
        }
    }
}