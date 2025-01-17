import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { ContentLoading } from 'src/app/models/ContentLoading';
import { IAppealStructure } from 'src/app/models/IAppealStructure';
import { ApiService } from 'src/app/services/api.service';
import { AuthService } from 'src/app/services/auth.service';
import { EnumManagerService } from 'src/app/services/enum-manager.service';
import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';
import { AppealStructureMode } from 'src/app/models/AppealStructureMode';

@Component({
  selector: 'app-guild-appeal-config',
  templateUrl: './guild-appeal-config.component.html',
  styleUrls: ['./guild-appeal-config.component.css']
})
export class GuildAppealConfigComponent implements OnInit {

  AppealStructureMode = AppealStructureMode;
  public guildId!: string;
  public newQuestionFormGroup!: FormGroup;
  maxLength4096 = { length: 4096 };

  public questions: ContentLoading<IAppealStructure[]> = { content: [], loading: true };

  constructor(private route: ActivatedRoute, private auth: AuthService, private toastr: ToastrService, private api: ApiService, public router: Router, private _formBuilder: FormBuilder, private dialog: MatDialog, private enumManager: EnumManagerService, private translator: TranslateService) { }

  ngOnInit(): void {
    this.guildId = this.route.snapshot.paramMap.get('guildid') as string;

    this.newQuestionFormGroup = this._formBuilder.group({
      question: ['', [ Validators.required, Validators.maxLength(4096) ]]
    });

    this.reload();
  }

  reload() {
    this.api.getSimpleData(`/guilds/${this.guildId}/appealstructures`).subscribe((data: IAppealStructure[]) => {
      this.questions.content = data;
      this.questions.loading = false;
    });
  }

  saveNewQuestion() {
    const data = {
      question: this.newQuestionFormGroup.value.question,
      sortOrder: Math.max(...this.questions.content ? this.questions.content.map(x => x.sortOrder) : [0], 0) + 1,
    };

    this.api.postSimpleData(`/guilds/${this.guildId}/appealstructures`, data).subscribe((data: IAppealStructure) => {
      if (this.questions.content) {
        this.questions.content.push(data);
      }
      this.newQuestionFormGroup.reset();
      Object.keys(this.newQuestionFormGroup.controls).forEach(key =>{
        this.newQuestionFormGroup.controls[key].setErrors(null)
      });
      this.toastr.success(this.translator.instant('AppealConfig.Created'));
    }, error => {
      console.error(error);
      this.toastr.error(this.translator.instant('AppealConfig.FailedToCreate'));
    });
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousIndex === event.currentIndex) return;
    moveItemInArray(this.questions.content as [], event.previousIndex, event.currentIndex);

    const data = this.questions.content?.map((element, index) => {
      return {
        id: element.id,
        sortOrder: index
      };
    });

    this.api.putSimpleData(`/guilds/${this.guildId}/appealstructures/reorder`, data).subscribe(() => {
      this.toastr.success(this.translator.instant('AppealConfig.Reordered'));
    }, error => {
      console.error(error);
      this.toastr.error(this.translator.instant('AppealConfig.FailedToReorder'));
      moveItemInArray(this.questions.content as [], event.currentIndex, event.previousIndex);
    });
  }

  onDelete(id: number) {
    if (this.questions.content) {
      this.questions.content = this.questions.content.filter(x => x.id !== id);
    }
  }

  get newQuestion() { return this.newQuestionFormGroup.get('question'); }
}
