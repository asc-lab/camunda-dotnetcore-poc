export class TaskDto {
    constructor(
        public taskId: string,
        public taskType: string,
        public taskName: string,
        public assignee: string,
        public orderId: string,
        public requestedSuperpower: string,
        public orderFrom: Date,
        public orderTo: Date,
        public customer: string,
        public orderStatus: string 
    ){}
}