import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Diagnostic } from 'src/app/models/diagnostic';
import { FacilitiesService } from 'src/app/services/facilities/facilities.service';

@Component({
  selector: 'app-facilities',
  templateUrl: './facilities.component.html',
  styleUrls: ['./facilities.component.scss']
})
export class FacilitiesComponent implements OnInit, OnDestroy {

  @Input('subscriberNumber') subscriberNumber: string;
  diagnostic?: Diagnostic = 
  {
    isConfigured: false,
    oltAdminState: false,
    oltOperState: false,
    ontAdminState: false,
    ontOperState: false,
    ontRxPower: false,
    ontTxPower: false,
    ontVoltage: false,
    facility: {
      subscriberNumber: '',
      ipAddress: '',
      nodeAddress: '',
      nodeName: ''
    }
  };
  diagnosticSubscription: Subscription;
  constructor(private facilityService: FacilitiesService) { }

  ngOnInit(): void {
    this.getDiagnostic();
  }


  ngOnDestroy(): void {
      this.diagnostic = null;
      this.diagnosticSubscription.unsubscribe();
  }

  getDiagnostic(){
    this.diagnosticSubscription = this.facilityService.getLastDiagnosticAndFacilities(this.subscriberNumber).subscribe(diagnostic => {
      if(diagnostic) {
        this.diagnostic = diagnostic;
      }
    }, (error: HttpErrorResponse)=>{
      this.showErrorDialog(error);
    });
  }

  showErrorDialog(error: HttpErrorResponse){
    console.log(error);
  }

}
