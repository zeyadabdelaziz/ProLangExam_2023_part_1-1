namespace Utilities;

public static class StringUtilities
{
	//task for 3
	public static int ToWords(string sentence)
	{
		string[] words = sentence.Split(' ');
		return words.Length;
	}

	//task for 4
	public static string ToSentence(string sentence)
	{
		return char.ToUpper(sentence[0]) + (sentence.ToLower()).Substring(1, sentence.Length - 1);
	}

	//task for 5

	public static string ToCamelCase(string sentence)
	{
		string[] words = sentence.Split(' ');
		string camelCase = "";

		for (int i = 0; i < words.Length; i++)
		{
			camelCase += ToSentence(words[i]);
		}

		return camelCase;
	}

	//task for 5
	public static string JoinWith(this IEnumerable<string> words, string separator)
	{
		return string.Join(separator, words);
	}

}