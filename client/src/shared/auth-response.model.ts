import { Author } from './author.model';
export interface AuthResponse {
  user: Author;
  jwt: { accessToken: string; refreshToken: string };
}
