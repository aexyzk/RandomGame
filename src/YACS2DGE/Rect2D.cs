namespace YACS2DGE.YACS2DGE
{
    public class Rect2D
    {
        public Vector2 Position = null;
        public Vector2 PivotPoint = null;
        public float Rotation = 0f;
        public Vector2 Scale = null;
        public string Tag = "";
        public Color FillColor = Color.Red();
        public int DeleteAfter = 0;

        public bool isDestroyed = false;

        public Rect2D(Vector2 Position, Vector2 Scale, string Tag, Color FillColor, int DeleteAfter = 0)
        {
            this.Position = Position;
            this.Rotation = 0;
            this.Scale = Scale;
            this.PivotPoint = new Vector2(Scale.x / 2, Scale.y / 2);  // Check if PivotPoint is null
            this.Tag = Tag;
            this.FillColor = FillColor;
            this.DeleteAfter = DeleteAfter;

            YACS2DGE.RegisterRect2D(this);
        }

        public void Tick(){
            if (DeleteAfter != 0){
                DeleteAfter--;
            }else{
                this.Destroy();
            }
        }

        public void Destroy(){
            YACS2DGE.UnregisterRect2D(this);
            isDestroyed = true;
        }
    }
}