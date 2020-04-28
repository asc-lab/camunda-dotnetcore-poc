import { Injectable } from '@angular/core';
import { TaskDto } from '../_model/task.dto';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';

@Injectable()
export class TaskService {
    constructor(private http: HttpClient) {}

    getMyTasks() : Promise<TaskDto[]> {
        return this.http.get<TaskDto[]>(`${environment.apiUrl}/Salesman/MyTasks`).toPromise();
    }
}