import { Injectable } from "@angular/core";
import { User } from "../../domain/entities/user.entity";
import { AuthDatasource } from "./auth.datasource";
import { HttpServiceBase } from "../../../core/services/http.base-service";
import { HttpParams } from "@angular/common/http";
import { UserModel } from "../models/user.model";

@Injectable()
export class AuthDatasourceImpl extends AuthDatasource {

    constructor(private httpClient: HttpServiceBase) { super(); }
    override async doLogin(email: string, password: string): Promise<User> {
        return await this.httpClient.post("/auth/login", new HttpParams().appendAll(
            {
                "email": email,
                "password": password
            }
        )).then((value) => UserModel.fromJson(value));
    }

}