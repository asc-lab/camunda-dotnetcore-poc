export class InvoiceDto {
    constructor(
        public invoiceId: string,
        public orderId: string,
        public customerCode: string,
        public customerName: string,
        public title: string,
        public total: number,
        public status: string
    ){}
}