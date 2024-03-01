import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ClassService } from '../../dashboard/class.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-attendance-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './attendance-list.component.html',
  styleUrl: './attendance-list.component.css',
})
export class AttendanceListComponent implements OnInit {
  title?: string;
  classId?: number;
  attendanceList?: any[];
  loading = true;

  constructor(
    public bsModalRef: BsModalRef,
    private classService: ClassService,
    private toast: ToastrService
  ) {}

  ngOnInit(): void {
    this.getList();
  }

  test() {
    // alert(this.attendanceList![0].createdDate);
    // alert(new Date('02/28/2024').toLocaleString('en-GB'));
    // alert(Date.parse(this.attendanceList![0].createdDate));
    // new Date(Math.max(...a.map(e => new Date(e.MeasureDate))));
    alert(this.attendanceList?.map((e) => new Date(e.createdDate)));
    alert(new Date());
  }

  getList() {
    this.classService.getAttendanceList(this.classId!).subscribe((res) => {
      if (res) {
        this.attendanceList = res;
        this.loading = false;
      }
    });
  }

  save() {}

  cancel() {
    this.bsModalRef.hide();
  }
}
