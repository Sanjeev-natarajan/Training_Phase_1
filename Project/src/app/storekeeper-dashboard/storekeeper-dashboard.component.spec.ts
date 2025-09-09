import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StorekeeperDashboardComponent } from './storekeeper-dashboard.component';

describe('StorekeeperDashboardComponent', () => {
  let component: StorekeeperDashboardComponent;
  let fixture: ComponentFixture<StorekeeperDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StorekeeperDashboardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StorekeeperDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
