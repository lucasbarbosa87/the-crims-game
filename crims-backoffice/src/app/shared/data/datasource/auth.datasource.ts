import { User } from "../../domain/entities/user.entity";

export abstract class AuthDatasource {
    abstract doLogin(email: string, password: string): Promise<User>;
}