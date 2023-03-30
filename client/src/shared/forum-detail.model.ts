import { Topic } from "./topic.model";

export interface ForumDetail {
  id: number;
  title: string;
  postCount: number;
  topicCount: number;
  imagePath: string;
  lastTopic: Topic;
}
