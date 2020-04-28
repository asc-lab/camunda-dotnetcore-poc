import { Component, OnInit } from '@angular/core';
import { TaskService } from '../_services/task-service';
import { TaskDto } from '../_model/task.dto';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  myTasks: TaskDto[];

  constructor(
    private route: ActivatedRoute,
    private taksService: TaskService) { }

  ngOnInit(): void {
    this.loadTasks();
  }

  async loadTasks() {
    this.myTasks = await this.taksService.getMyTasks();
  }

}
