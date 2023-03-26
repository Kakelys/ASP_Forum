import { ForumDetail } from './forum-detail.model';

export interface SectionDetail {
  id: number;
  title: string;
  orderNumber: number;
  forums: ForumDetail[];
}
