import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { TaskDto } from '../_model/task.dto';

@Injectable()
export class TaskService {
    constructor(private http: HttpClient) {}
    
    getCustomerTasks(): Promise<TaskDto[]> {
        return this.http.get<TaskDto[]>(`${environment.apiUrl}/Client/MyTasks`).toPromise();
    }
}