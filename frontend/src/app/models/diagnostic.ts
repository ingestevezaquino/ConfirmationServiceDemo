import { Facility } from "./facility";

export interface Diagnostic{
    isConfigured: boolean;
    oltAdminState: boolean;
    oltOperState: boolean;
    ontAdminState: boolean;
    ontOperState: boolean;
    ontRxPower: boolean;
    ontTxPower: boolean;
    ontVoltage: boolean;
    facility: Facility;
}