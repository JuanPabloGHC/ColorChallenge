
public class GameData
{
    public bool[] openLevels = new bool[4];
    public bool ganador;
    public int[] highScores = new int[4];

    public GameData(GController controller)
    {
        openLevels = controller.OpenLevels;
        ganador = controller.Ganador;
        highScores = controller.HighScores;
    }
}
