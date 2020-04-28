import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '@environments/environment';

@Injectable()
export class SuperPowerService {
    constructor(private http: HttpClient) {
        
    }

    getAllPowers() : Observable<any> {
        return this.http.get(`${environment.apiUrl}/Superpower`);
    }
}