import { Injectable } from "@angular/core";
import { User } from "../../domain/entities/user.entity";
import { AuthRepository } from "../../domain/repositories/auth.repository";
import { AuthDatasource } from "../datasource/auth.datasource";

@Injectable()
export class AuthRepositoryImpl extends AuthRepository {

    constructor(private authDatasource: AuthDatasource) { super() }

    override async doLogin(email: string, password: string): Promise<User> {
        var data = this.authDatasource.doLogin(email, password);
        return data;
    }

}