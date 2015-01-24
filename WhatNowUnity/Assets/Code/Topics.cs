using System;
using System.Collections.Generic;

namespace Topics
{
	class TopicSet {
		private List<Topic> allTopics;
		static Random rnd = new Random();

		public TopicSet() {
			allTopics = new List<Topic> ();

			Topic videoGames = new Topic ("Video Games");
			Topic cars = new Topic ("Cars");
			Topic makeup = new Topic ("Makeup");
			Topic clothes = new Topic ("Clothes");
			Topic marklar = new Topic ("Marklar");

			videoGames.addTopic (cars);
			videoGames.addTopic (makeup);
			videoGames.addTopic (clothes);
			videoGames.addTopic (marklar);

			cars.addTopic (videoGames);
			cars.addTopic (makeup);
			cars.addTopic (clothes);
			cars.addTopic (marklar);

			makeup.addTopic (videoGames);
			makeup.addTopic (cars);
			makeup.addTopic (clothes);
			makeup.addTopic (marklar);

			clothes.addTopic (videoGames);
			clothes.addTopic (cars);
			clothes.addTopic (makeup);
			clothes.addTopic (marklar);

			marklar.addTopic (videoGames);
			marklar.addTopic (cars);
			marklar.addTopic (makeup);
			marklar.addTopic (clothes);

			allTopics.Add (videoGames);
			allTopics.Add (cars);
			allTopics.Add (makeup);
			allTopics.Add (clothes);
			allTopics.Add (marklar);
		}

		public Topic getStartingTopic() {
			return allTopics [rnd.Next (allTopics.Count)];
		}
	}

	class Topic
	{
		private List<Topic> relatedTopics;

		private String topicName;

		public List<Topic> getPossibleTopics() {
			Console.WriteLine ("Getting Possible Topics");
			return relatedTopics;
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
			Console.WriteLine (nextTopics.ToString());
			foreach (Topic currTopic in nextTopics) // Loop through List with foreach.
			{
				Console.WriteLine(currTopic.getTopicName());
			}
		}
	}
}