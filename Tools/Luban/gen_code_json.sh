#!/bin/zsh
WORKSPACE=../..

GEN_CLIENT=${WORKSPACE}/Tools/Luban/Tools/Luban.ClientServer/Luban.ClientServer.dll
CONF_ROOT=${WORKSPACE}/Configs/Luban


dotnet ${GEN_CLIENT} --template_search_path CustomTemplate -j cfg --\
 -d ${CONF_ROOT}/Defines/__root__.xml \
 --input_data_dir ${CONF_ROOT}/Datas \
 --output_code_dir ${WORKSPACE}/Assets/Scripts/Hotfix/Model/Generate/Luban \
 --output_data_dir ${WORKSPACE}/Assets/Res/Luban \
 --gen_types code_cs_unity_json,data_json \
 -s all 
