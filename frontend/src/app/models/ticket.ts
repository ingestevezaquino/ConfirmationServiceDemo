export interface Ticket {
    id: number;
    processName: string;
    productType: string;
    caseNumber: string;
    subscriberNumber: string;
    currentQueue: string;
    destinationQueue?: any;
    uac: string;
    clientName: string;
    clientContactPhone: string;
    status: string;
    creationTime: Date;
    username: string;
}