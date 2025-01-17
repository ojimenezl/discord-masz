export interface AutoModRuleDefinition {
    type: number;
    key: string;
    showLimitField: boolean;
    showTimeLimitField: boolean;
    timeLimitFieldMessage?: boolean;
    showCustomField?: boolean;
    tooltip?: boolean;
    link?: string;
    requireCustomField?: boolean;
}