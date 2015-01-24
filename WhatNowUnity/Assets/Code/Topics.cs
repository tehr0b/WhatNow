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

		videoGames.addTopic (cars);
		videoGames.addTopic (makeup);
		videoGames.addTopic (clothes);
		videoGames.addTopic (marklar);
		videoGames.addTopic (outerSpace);
		videoGames.addTopic (milk);

		cars.addTopic (videoGames);
		cars.addTopic (makeup);
		cars.addTopic (clothes);
		cars.addTopic (marklar);
		cars.addTopic (outerSpace);
		cars.addTopic (milk);

		makeup.addTopic (videoGames);
		makeup.addTopic (cars);
		makeup.addTopic (clothes);
		makeup.addTopic (marklar);
		makeup.addTopic (outerSpace);
		makeup.addTopic (milk);

		clothes.addTopic (videoGames);
		clothes.addTopic (cars);
		clothes.addTopic (makeup);
		clothes.addTopic (marklar);
		clothes.addTopic (outerSpace);
		clothes.addTopic (milk);

		marklar.addTopic (videoGames);
		marklar.addTopic (cars);
		marklar.addTopic (makeup);
		marklar.addTopic (clothes);
		marklar.addTopic (outerSpace);
		marklar.addTopic (milk);

		outerSpace.addTopic (videoGames);
		outerSpace.addTopic (cars);
		outerSpace.addTopic (makeup);
		outerSpace.addTopic (clothes);
		outerSpace.addTopic (marklar);
		outerSpace.addTopic (milk);

		milk.addTopic (videoGames);
		milk.addTopic (cars);
		milk.addTopic (makeup);
		milk.addTopic (clothes);
		milk.addTopic (marklar);
		milk.addTopic (outerSpace);

		allTopics.Add (videoGames);
		allTopics.Add (cars);
		allTopics.Add (makeup);
		allTopics.Add (clothes);
		allTopics.Add (marklar);
		allTopics.Add (outerSpace);
		allTopics.Add (milk);
	}

	public Topic getStartingTopic() {
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

	public List<Topic> getPossibleTopics() {
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

	public void addTopic(Topic topic) {
		relatedTopics.Add (topic);
	}

	public String getTopicName() {
		return topicName;
	}
}

class MainClass
{
	public static void Main (string[] args)
	{
		Console.WriteLine ("Hello World!");
		TopicSet topics = new TopicSet ();
		Topic topic = topics.getStartingTopic ();
		Console.WriteLine (topic.getTopicName());
		Console.WriteLine ("Related Topics");
		List<Topic> nextTopics = topic.getPossibleTopics ();
		foreach (Topic currTopic in nextTopics) // Loop through List with foreach.
		{
			Console.WriteLine(currTopic.getTopicName());
		}
	}
}