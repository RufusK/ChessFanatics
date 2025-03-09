namespace Core.Models;

public enum GameResult
{
    None,
    WhiteWin,
    BlackWin,
    Draw,
    Bye // Player gets a free point due to an odd number of participants
}