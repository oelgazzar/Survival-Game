public interface ISaveable {
    public string SaveID { get; }

    public string Save();

    public void Load(string state);
}
