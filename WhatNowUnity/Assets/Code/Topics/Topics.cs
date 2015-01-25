using System;
using System.Collections.Generic;

public enum TopicName
{
	NOTHING,
	Cooking,
	Fish,
	Volleyball,
	Clothes,
	Bugs,
	Drinking,
	Internet,
	Shopping,
	Teacup,
	Motorcycles,
	Lollipop,
	Hats,
	Gaming,
	Eating,
	Basketball,
	Cars,
	Leaf,
	Book,
	Biking,
	Cats,
	Hiking,
	Computers,
	Movies,
	Tennis,
	Coffee,
	TV,
	Microscope,
	Shoes,
	Milk,
	Teapot,
	Weather,
	Painting,
	Undershirts,
	Dresses,
	Raddish,
	Photography,
	Heels,
	Music,
	AtomicPower,
	Planes,
	Sunsets,
	Lighthouse,
	Ironing,
	Waves,
	Smoking,
	Books,
	Recycling,
	Humans,
	MAX //Not an actual topic, just used for iteration count
}

public class TopicSet {
	private List<Topic> allTopics;
	private Random rnd = new Random();

	public TopicSet() {
		allTopics = new List<Topic> ();

		Topic videoGames = new Topic (TopicName.Gaming);
		Topic cars = new Topic (TopicName.Cars);
		Topic motorcycles = new Topic (TopicName.Motorcycles);
		Topic clothes = new Topic (TopicName.Clothes);
		Topic marklar = new Topic (TopicName.AtomicPower);
		Topic microscope = new Topic (TopicName.Microscope);
		Topic milk = new Topic (TopicName.Milk);

		videoGames.AddTopic (cars);
		videoGames.AddTopic (motorcycles);
		videoGames.AddTopic (clothes);
		videoGames.AddTopic (marklar);
		videoGames.AddTopic (microscope);
		videoGames.AddTopic (milk);

		cars.AddTopic (videoGames);
		cars.AddTopic (motorcycles);
		cars.AddTopic (clothes);
		cars.AddTopic (marklar);
		cars.AddTopic (microscope);
		cars.AddTopic (milk);

		motorcycles.AddTopic (videoGames);
		motorcycles.AddTopic (cars);
		motorcycles.AddTopic (clothes);
		motorcycles.AddTopic (marklar);
		motorcycles.AddTopic (microscope);
		motorcycles.AddTopic (milk);

		clothes.AddTopic (videoGames);
		clothes.AddTopic (cars);
		clothes.AddTopic (motorcycles);
		clothes.AddTopic (marklar);
		clothes.AddTopic (microscope);
		clothes.AddTopic (milk);

		marklar.AddTopic (videoGames);
		marklar.AddTopic (cars);
		marklar.AddTopic (motorcycles);
		marklar.AddTopic (clothes);
		marklar.AddTopic (microscope);
		marklar.AddTopic (milk);

		microscope.AddTopic (videoGames);
		microscope.AddTopic (cars);
		microscope.AddTopic (motorcycles);
		microscope.AddTopic (clothes);
		microscope.AddTopic (marklar);
		microscope.AddTopic (milk);

		milk.AddTopic (videoGames);
		milk.AddTopic (cars);
		milk.AddTopic (motorcycles);
		milk.AddTopic (clothes);
		milk.AddTopic (marklar);
		milk.AddTopic (microscope);

		allTopics.Add (videoGames);
		allTopics.Add (cars);
		allTopics.Add (motorcycles);
		allTopics.Add (clothes);
		allTopics.Add (marklar);
		allTopics.Add (microscope);
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

public class Topic
{
	private List<Topic> relatedTopics;
	private TopicName topicName;
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

	public Topic(TopicName name) {
		topicName = name;
		relatedTopics = new List<Topic> ();
	}

	public void AddTopic(Topic topic) {
		relatedTopics.Add (topic);
	}

	public String GetTopicName() {
		return topicName.ToString ();;
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