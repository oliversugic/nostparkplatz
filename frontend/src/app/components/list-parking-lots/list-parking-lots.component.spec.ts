import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListParkingLotsComponent } from './list-parking-lots.component';

describe('ListParkingLotsComponent', () => {
  let component: ListParkingLotsComponent;
  let fixture: ComponentFixture<ListParkingLotsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListParkingLotsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListParkingLotsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
