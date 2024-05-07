import { User } from "../entities/user.entity";

export abstract class AuthRepository {
    abstract doLogin(email: string, password: string): Promise<User>;
}