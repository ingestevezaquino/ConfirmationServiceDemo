import { Input, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { TooltipModule } from 'primeng/tooltip';
import { CardModule } from 'primeng/card';
import { DividerModule } from 'primeng/divider';
import { ToolbarModule } from 'primeng/toolbar';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    TableModule,
    InputTextModule,
    ButtonModule,
    TooltipModule,
    CardModule,
    DividerModule,
    ToolbarModule
  ],
  exports:[
    TableModule,
    InputTextModule,
    ButtonModule,
    TooltipModule,
    CardModule,
    DividerModule,
    ToolbarModule
  ]
})
export class PrimeModule { }
