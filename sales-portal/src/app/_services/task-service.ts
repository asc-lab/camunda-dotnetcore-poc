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

    claimTask(taskId: string) : Promise<TaskDto> {
        return this.http.post<TaskDto>(`${environment.apiUrl}/Salesman/MyTasks/${taskId}/claim`,{}).toPromise();
    }
}