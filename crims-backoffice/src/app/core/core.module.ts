import { NgModule } from "@angular/core";
import { HttpServiceBase } from "./services/http.base-service";
import { HttpService } from "./services/http.implementation";
import { StorageService, StorageServiceImpl } from "./services/storage-service";
import { UserSessionService, UserSessionServiceImpl } from "../shared/services/user-session.service";
import { GetUserUsecase } from "../shared/domain/usecases/get-user.usecase";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { BaseUrlInterceptor } from "./services/interceptors/base-url.interceptor";

@NgModule({
    imports: [
        HttpClientModule,
    ],
    providers: [
        { provide: HttpServiceBase, useClass: HttpService },
        { provide: StorageService, useClass: StorageServiceImpl },
        { provide: UserSessionService, useClass: UserSessionServiceImpl },
        { provide: HTTP_INTERCEPTORS, useClass: BaseUrlInterceptor, multi: true },
        { provide: GetUserUsecase },
    ]
})

export class CoreModule { }