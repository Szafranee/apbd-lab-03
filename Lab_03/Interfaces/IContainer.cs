namespace Lab_03.Interfaces;

public interface IContainer
{
    double MaxLoad { get; }
    double Height { get; }
    double OwnWeight { get; }
    double Depth { get; }
    string SerialNumber { get; }
    void Load(double loadWeight);
    void Unload();
}