

/// An IRankCalculatable offers a function to determine what level should be used for an instance based on an arbritary playerLevel
interface IRankCalculatable{

	int GetRank(int playerLevel);

}