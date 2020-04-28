import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HeroDto } from '../_model/hero.dto';
import { environment } from '@environments/environment';

@Injectable()
export class HeroService {
    constructor(private http: HttpClient) {
        
    }

    findAvailableHeroes(orderId: string) : Promise<HeroDto[]> {
        return this.http.get<HeroDto[]>(`${environment.apiUrl}/Heroes/AvailableForOrder/`+ orderId).toPromise();
    }
}