import { Topic } from "./topic.model";

export interface ForumDetail {
  postCount: number;
  topicCount: number;
  imagePath: string;
  lastTopic: Topic;
}
