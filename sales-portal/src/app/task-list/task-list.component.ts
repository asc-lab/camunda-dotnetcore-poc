import { Component, OnInit } from '@angular/core';
import { TaskService } from '../_services/task-service';
import { TaskDto } from '../_model/task.dto';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../_services/auth-service';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  myTasks: TaskDto[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private auth: AuthService,
    private taksService: TaskService) { }

  ngOnInit(): void {
    this.loadTasks();
  }

  async loadTasks() {
    this.myTasks = await this.taksService.getMyTasks();
  }

  async claimTask(task: TaskDto) {
    const updatedTask = await this.taksService.claimTask(task.taskId);
    task.assignee = updatedTask.assignee;
  }

  performTask(task: TaskDto) {
    switch (task.taskType) {
      case 'Task_PrepareOffer':
        this.router.navigate(['/create-offer', task.orderId, task.taskId]);
        break;
      default:
        alert('Unexpected task type!')
    }
  }

  hasAction(task: TaskDto, action: string) {
    return task.actions && task.actions.indexOf(action)!=-1;
  }

  isAssigneeCurrentUser(task: TaskDto) {
    return task.assignee && task.assignee==this.auth.currentUserValue.user;
  }

}
