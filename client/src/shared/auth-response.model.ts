import { User } from './user.model';
export interface AuthResponse {
  user: User;
  role: string;
  jwt: { accessToken: string; refreshToken: string };
}
