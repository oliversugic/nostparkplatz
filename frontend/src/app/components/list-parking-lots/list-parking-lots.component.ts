import { Component, OnInit } from '@angular/core';
import { ParkingLot } from 'src/app/models/parking-lot';
import { ExamService } from 'src/app/services/exam.service';

@Component({
  selector: 'app-list-parking-lots',
  templateUrl: './list-parking-lots.component.html',
  styleUrls: ['./list-parking-lots.component.css']
})
export class ListParkingLotsComponent implements OnInit {
 parkingLots:ParkingLot[]=[];

  constructor(private examService:ExamService) { }

  ngOnInit(): void {
    this.examService.getParkingLots().subscribe((data:ParkingLot[])=>{
      this.parkingLots = data;
    });
  }

}
