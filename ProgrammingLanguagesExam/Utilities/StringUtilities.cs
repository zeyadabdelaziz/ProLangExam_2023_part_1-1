namespace Utilities;

public static class StringUtilities
{
	public static int ToWords(string sentence)
	{
		string[] words = sentence.Split(' ');
		return words.Length;
	}

	public static string ToSentence(string sentence)
	{
		sentence = char.ToUpper(sentence[0]) + (sentence.ToLower()).Substring(1, sentence.Length - 1);
		return sentence;
	}

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

	public static string JoinWith(this IEnumerable<string> words, string separator)
	{
		return string.Join(separator, words);
	}

}