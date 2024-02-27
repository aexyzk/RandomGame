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
        public int DeleteAfter = 0;
        public Color FillColor = Color.White();

        public bool isDestroyed = false;

        public Sprite2D(Vector2 _Position, Vector2 _Scale, string _Tag, string _SpriteTextureFileName, int _DeleteAfter = 0, Color _FillColor = null)
        {
            this.Position = new Vector2(_Position.x, _Position.y);
            this.Rotation = 0;
            this.Scale = new Vector2(_Scale.x, _Scale.y);
            this.Tag = _Tag;
            this.SpriteTextureFileName = _SpriteTextureFileName;
            this.DeleteAfter = _DeleteAfter;
            this.FillColor = _FillColor ?? Color.White();

            YACS2DGE.RegisterSprite2D(this);
        }

        public void Tick(){
            if (DeleteAfter != 0){
                DeleteAfter--;
            }else{
                this.Destroy();
            }
        }

        public void Destroy(){
            YACS2DGE.UnregisterSprite2D(this);
            isDestroyed = true;
        }
    }
}