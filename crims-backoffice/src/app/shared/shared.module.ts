import { NgModule } from "@angular/core";
import { AuthDatasource } from "./data/datasource/auth.datasource";
import { AuthDatasourceImpl } from "./data/datasource/auth.datasource.implementation";
import { AuthRepository } from "./domain/repositories/auth.repository";
import { AuthRepositoryImpl } from "./data/repositories/auth.repository.implementation";

@NgModule({
    providers: [{ provide: AuthDatasource, useClass: AuthDatasourceImpl },
    { provide: AuthRepository, useClass: AuthRepositoryImpl },
    ],
})
export class SharedModule { }