import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class SuperPowerService {
    constructor(private http: HttpClient) {
        
    }

    getAllPowers() : Observable<any> {
        return this.http.get('http://localhost:6080/Superpower');
    }
}