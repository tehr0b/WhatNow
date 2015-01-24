using System;
using System.Collections.Generic;


class TopicSet {
	private List<Topic> allTopics;
	private Random rnd = new Random();

	public TopicSet() {
		allTopics = new List<Topic> ();

		Topic videoGames = new Topic ("Video Games");
		Topic cars = new Topic ("Cars");
		Topic makeup = new Topic ("Makeup");
		Topic clothes = new Topic ("Clothes");
		Topic marklar = new Topic ("Marklar");
		Topic outerSpace = new Topic ("Outer Space");
		Topic milk = new Topic ("Milk");

		videoGames.AddTopic (cars);
		videoGames.AddTopic (makeup);
		videoGames.AddTopic (clothes);
		videoGames.AddTopic (marklar);
		videoGames.AddTopic (outerSpace);
		videoGames.AddTopic (milk);

		cars.AddTopic (videoGames);
		cars.AddTopic (makeup);
		cars.AddTopic (clothes);
		cars.AddTopic (marklar);
		cars.AddTopic (outerSpace);
		cars.AddTopic (milk);

		makeup.AddTopic (videoGames);
		makeup.AddTopic (cars);
		makeup.AddTopic (clothes);
		makeup.AddTopic (marklar);
		makeup.AddTopic (outerSpace);
		makeup.AddTopic (milk);

		clothes.AddTopic (videoGames);
		clothes.AddTopic (cars);
		clothes.AddTopic (makeup);
		clothes.AddTopic (marklar);
		clothes.AddTopic (outerSpace);
		clothes.AddTopic (milk);

		marklar.AddTopic (videoGames);
		marklar.AddTopic (cars);
		marklar.AddTopic (makeup);
		marklar.AddTopic (clothes);
		marklar.AddTopic (outerSpace);
		marklar.AddTopic (milk);

		outerSpace.AddTopic (videoGames);
		outerSpace.AddTopic (cars);
		outerSpace.AddTopic (makeup);
		outerSpace.AddTopic (clothes);
		outerSpace.AddTopic (marklar);
		outerSpace.AddTopic (milk);

		milk.AddTopic (videoGames);
		milk.AddTopic (cars);
		milk.AddTopic (makeup);
		milk.AddTopic (clothes);
		milk.AddTopic (marklar);
		milk.AddTopic (outerSpace);

		allTopics.Add (videoGames);
		allTopics.Add (cars);
		allTopics.Add (makeup);
		allTopics.Add (clothes);
		allTopics.Add (marklar);
		allTopics.Add (outerSpace);
		allTopics.Add (milk);
	}

	public Topic GetStartingTopic() {
		return allTopics [rnd.Next (allTopics.Count)];
	}

	public List<Topic> GetNRandomTopics(int n)
	{
		List<Topic> nextTopics = new List<Topic>();
		int topicsNeeded = n;
		int topicsSeen = 0;
		foreach (Topic topic in allTopics)
		{
			if (rnd.Next(allTopics.Count -topicsSeen) <= (topicsNeeded-1))
			{
				nextTopics.Add(topic);
				topicsNeeded--;
			}
			topicsSeen++;
		}
		return nextTopics;
	}
}

class Topic
{
	private List<Topic> relatedTopics;
	private String topicName;
	private Random rnd = new Random();

	public List<Topic> GetPossibleTopics() {
		Console.WriteLine ("Getting Possible Topics");
		return GetNRandomTopics (4);
	}

	private List<Topic> GetNRandomTopics (int n)
	{
		List<Topic> nextTopics = new List<Topic>();
		int topicsNeeded = n;
		int topicsSeen = 0;
		foreach (Topic topic in relatedTopics)
		{
			if (rnd.Next(relatedTopics.Count -topicsSeen) <= (topicsNeeded-1))
			{
				nextTopics.Add(topic);
				topicsNeeded--;
			}
			topicsSeen++;
		}
		return nextTopics;
	}

	public Topic(String name) {
		topicName = name;
		relatedTopics = new List<Topic> ();
	}

	public void AddTopic(Topic topic) {
		relatedTopics.Add (topic);
	}

	public String GetTopicName() {
		return topicName;
	}
}

class MainClass
{
	public static void Main (string[] args)
	{
		Console.WriteLine ("Hello World!");
		TopicSet topics = new TopicSet ();
		Topic topic = topics.GetStartingTopic ();
		Console.WriteLine (topic.GetTopicName());
		Console.WriteLine ("Related Topics");
		List<Topic> nextTopics = topic.GetPossibleTopics ();
		foreach (Topic currTopic in nextTopics) // Loop through List with foreach.
		{
			Console.WriteLine(currTopic.GetTopicName());
		}
	}
}