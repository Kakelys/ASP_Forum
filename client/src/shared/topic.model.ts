import { Author } from "./author.model";

export interface Topic {
  id: number;
  forumId: number;
  title: string;
  isPinned: boolean;
  isClosed: boolean;
  createDate: Date;
  author: Author;
}
