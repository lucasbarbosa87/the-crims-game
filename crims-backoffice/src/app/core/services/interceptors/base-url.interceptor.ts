import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";

@Injectable()
export class BaseUrlInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const baseUrl = "http://localhost:8081";
        let httpHeader = new HttpHeaders({ 'Content-Type': 'application/json' })
        let request = req.clone({
            headers: httpHeader,
            url: baseUrl + req.url
        });
        return next.handle(request);
    }
}