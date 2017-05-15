namespace SkyTimer.Model
{
    public class UpdateScrambleInstruction
    {
        public UpdateScrambleInstruction(string name)
        {
            CubeName = name;
        }
        public string CubeName { get; set; }
    }
}
