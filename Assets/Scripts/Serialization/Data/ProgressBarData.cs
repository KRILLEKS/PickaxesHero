[System.Serializable]
public class ProgressBarData
{
    public int value;

    public ProgressBarData(ProgressBar progressBar) =>
        value = progressBar.value;
}
