import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LandingComponent } from './landing/landing.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ClassService } from './class.service';
import { StudentListComponent } from './student-list/student-list.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    children: [
      { path: '', redirectTo: '/dashboard/landing', pathMatch: 'full' },
      { path: 'landing', component: LandingComponent },
      {
        path: 'class-detail/:id',
        component: StudentListComponent,
      },
    ],
  },
];

@NgModule({
  declarations: [],
  imports: [
    DashboardComponent,
    LandingComponent,
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
  ],
  providers: [ClassService],
})
export class DashboardModule {}
