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

        public Rect2D(Vector2 Position, Vector2 Scale, string Tag, Color FillColor)
        {
            this.Position = Position;
            this.Rotation = 0;
            this.Scale = Scale;
            this.PivotPoint = new Vector2(Scale.x / 2, Scale.y / 2);  // Check if PivotPoint is null
            this.Tag = Tag;
            this.FillColor = FillColor;

            Log.Info($"Created Rect2D: {Tag}");
            YACS2DGE.RegisterRect2D(this);
        }

        public void Destroy(){
            YACS2DGE.UnregisterRect2D(this);
        }
    }
}