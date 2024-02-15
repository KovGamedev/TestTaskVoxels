using System.Collections.Generic;

public enum TagType : byte
{
	Character = 0,
	SinglePlace = 1,
	EnemyCharacter = 2
}

public static class TagManager
{
	private static readonly Dictionary<TagType, string> _tags;

	static TagManager()
	{
		_tags = new Dictionary<TagType, string>
			{
				{TagType.Character, "Character"},
				{TagType.EnemyCharacter, "EnemyCharacter"},
				{TagType.SinglePlace, "SinglePlace"}
			};
	}

	public static string GetTag(TagType tagType)
	{
		return _tags[tagType];
	}
}